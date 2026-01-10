import { Alert, Backdrop, Box, CircularProgress, Snackbar, Typography } from "@mui/material";
import collectionStyles from "./collectionStyles";
import MainButton from "../../components/mainButton/MainButton";
import { AddCircle } from "@mui/icons-material";
import ListItem, { ListEmpty, ListItemSkeleton } from "../../components/listItem/ListItem";
import CollectionController from "./collectionController";
import { useInfiniteQuery, useMutation } from "@tanstack/react-query";
import { useState } from "react";

export default function Collection() {
    const controller = new CollectionController();
    const path = ["paulo", "jogos"];
    const [message, setMessage] = useState("");

    const infiniteQuery = useInfiniteQuery({
        queryKey: [path.join("/")],
        queryFn: () => controller.list(path[0], path[1], 0),
        getNextPageParam: (lastPage, allPages) => {
            return 0;
        },
        initialPageParam: 0,
    });

    const deleteMutation = useMutation({
        mutationFn: ({ registerId }: { registerId: string }) =>
            controller.delete(path[0], path[1], registerId),
        onSuccess: () => {
            infiniteQuery.refetch();
            setMessage("Register deleted successfully");
        },
        onError: () => {
            setMessage("Error deleting register");
        },
    });

    const updateMutation = useMutation({
        mutationFn: ({ registerId, data }: { registerId: string; data: object }) =>
            controller.update(path[0], path[1], registerId, data),
        onSuccess: () => {
            infiniteQuery.refetch();
            setMessage("Register updated successfully");
        },
        onError: () => {
            setMessage("Error updating register");
        },
    });

    const deleteRegister = async (registerId: string) => {
        deleteMutation.mutate({ registerId });
    };

    const updateRegister = async (registerId: string, data: object) => {
        updateMutation.mutate({ registerId, data });
    };

    return (
        <Box sx={collectionStyles.page}>
            <Box sx={collectionStyles.titleBox}>
                <Typography sx={collectionStyles.title} variant="h1">
                    Database: {path[0]} {`>`} Collection: {path[1]}
                </Typography>
                <MainButton text="ADD DATA" icon={<AddCircle sx={{ fontSize: "24px" }} />} />
            </Box>

            <Box sx={collectionStyles.listBox}>
                {infiniteQuery.isFetching && <ListItemSkeleton />}
                {infiniteQuery.data?.pages.flat().length === 0 && <ListEmpty />}
                {infiniteQuery.data?.pages.flat().map((item, index) => (
                    <ListItem
                        key={index}
                        data={item!}
                        deleteRegister={deleteRegister}
                        updateRegister={updateRegister}
                    />
                ))}
            </Box>

            <Backdrop
                sx={(theme) => ({ color: "#fff", zIndex: theme.zIndex.drawer + 1 })}
                open={deleteMutation.isPending}
            >
                <CircularProgress color="inherit" />
            </Backdrop>

            <Snackbar
                open={deleteMutation.isSuccess || updateMutation.isSuccess}
                autoHideDuration={6000}
            >
                <Alert severity="success" variant="filled" sx={{ width: "100%" }}>
                    {message}
                </Alert>
            </Snackbar>

            <Snackbar
                open={deleteMutation.isError || updateMutation.isError}
                autoHideDuration={6000}
            >
                <Alert severity="error" variant="filled" sx={{ width: "100%" }}>
                    {message}
                </Alert>
            </Snackbar>
        </Box>
    );
}
