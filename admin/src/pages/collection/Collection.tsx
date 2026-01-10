import { Alert, Backdrop, Box, CircularProgress, Snackbar, Typography } from "@mui/material";
import collectionStyles from "./collectionStyles";
import MainButton from "../../components/mainButton/MainButton";
import { AddCircle } from "@mui/icons-material";
import ListItem, { ListEmpty, ListItemSkeleton } from "../../components/listItem/ListItem";
import CollectionController from "./collectionController";
import { keepPreviousData, useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useState } from "react";
import Pagination from "@mui/material/Pagination";
import Stack from "@mui/material/Stack";

export default function Collection() {
    const PAGE_SIZE = 10;
    const controller = new CollectionController();
    const queryClient = useQueryClient();
    const path = ["paulo", "jogos"];
    const [message, setMessage] = useState("");
    const [page, setPage] = useState(1);

    const query = useQuery({
        queryKey: [path.join("/"), page],
        queryFn: () => controller.list(path[0], path[1], page - 1, PAGE_SIZE),
        placeholderData: keepPreviousData,
    });

    const deleteMutation = useMutation({
        mutationFn: ({ registerId }: { registerId: string }) =>
            controller.delete(path[0], path[1], registerId),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: [path.join("/")] });
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
            queryClient.invalidateQueries({ queryKey: [path.join("/")] });
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

    const handlePageChange = (event: React.ChangeEvent<unknown>, value: number) => {
        setPage(value);
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
                {query.isPending && <ListItemSkeleton />}
                {query.data?.length === 0 && <ListEmpty />}
                {query.data?.map((item, index) => (
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
            <Stack spacing={2} alignSelf="center">
                <Stack spacing={2} alignSelf="center">
                    <Pagination
                        count={query.data?.length === PAGE_SIZE ? page + 1 : page}
                        page={page}
                        onChange={handlePageChange}
                    />
                </Stack>
            </Stack>
        </Box>
    );
}
