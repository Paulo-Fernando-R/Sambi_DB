import { Alert, Backdrop, Box, CircularProgress, Snackbar, Typography } from "@mui/material";
import collectionStyles from "./collectionStyles";
import MainButton from "../../components/mainButton/MainButton";
import { AddCircle } from "@mui/icons-material";
import ListItem, { ListEmpty, ListItemSkeleton } from "../../components/listItem/ListItem";
import CollectionController from "./collectionController";
import { useInfiniteQuery, useMutation } from "@tanstack/react-query";

export default function Collection() {
    const controller = new CollectionController();
    const path = ["paulo", "jogos"];

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
        },
    });

    const deleteRegister = async (registerId: string) => {
        deleteMutation.mutate({ registerId });
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
                    <ListItem key={index} data={item!} deleteRegister={deleteRegister} />
                ))}
            </Box>

            <Backdrop
                sx={(theme) => ({ color: "#fff", zIndex: theme.zIndex.drawer + 1 })}
                open={deleteMutation.isPending}
            >
                <CircularProgress color="inherit" />
            </Backdrop>

            <Snackbar open={deleteMutation.isSuccess} autoHideDuration={6000}>
                <Alert severity="success" variant="filled" sx={{ width: "100%" }}>
                    Register deleted successfully
                </Alert>
            </Snackbar>

            <Snackbar open={deleteMutation.isError} autoHideDuration={6000}>
                <Alert severity="error" variant="filled" sx={{ width: "100%" }}>
                    Error deleting register
                </Alert>
            </Snackbar>
        </Box>
    );
}
