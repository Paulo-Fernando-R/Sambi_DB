import { Backdrop, Box, CircularProgress, Snackbar, Alert } from "@mui/material";
import collectionStyles from "./collectionStyles";
import CollectionHeader from "./components/CollectionHeader";
import CollectionList from "./components/CollectionList";
import CollectionPagination from "./components/CollectionPagination";
import { useCollection } from "./useCollection";

export default function Collection() {
    const path = ["paulo", "jogos"];
    const {
        query,
        deleteMutation,
        updateMutation,
        insertMutation,
        page,
        message,
        PAGE_SIZE,
        deleteRegister,
        updateRegister,
        insertRegister,
        handlePageChange,
    } = useCollection(path[0], path[1]);

    return (
        <Box sx={collectionStyles.page}>
            <CollectionHeader
                databaseName={path[0]}
                collectionName={path[1]}
                insertRegister={insertRegister}
            />

            <CollectionList
                data={query.data}
                isLoading={query.isPending}
                deleteRegister={deleteRegister}
                updateRegister={updateRegister}
            />

            <Backdrop
                sx={(theme) => ({ color: "#fff", zIndex: theme.zIndex.drawer + 1 })}
                open={deleteMutation.isPending}
            >
                <CircularProgress color="inherit" />
            </Backdrop>

            <Snackbar
                open={
                    deleteMutation.isSuccess || updateMutation.isSuccess || insertMutation.isSuccess
                }
                autoHideDuration={6000}
            >
                <Alert severity="success" variant="filled" sx={{ width: "100%" }}>
                    {message}
                </Alert>
            </Snackbar>

            <Snackbar
                open={deleteMutation.isError || updateMutation.isError || insertMutation.isError}
                autoHideDuration={6000}
            >
                <Alert severity="error" variant="filled" sx={{ width: "100%" }}>
                    {message}
                </Alert>
            </Snackbar>

            <CollectionPagination
                count={query.data?.length === PAGE_SIZE ? page + 1 : page}
                page={page}
                onChange={handlePageChange}
            />
        </Box>
    );
}
