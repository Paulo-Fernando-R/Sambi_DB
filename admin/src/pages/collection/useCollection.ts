import { keepPreviousData, useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useState } from "react";
import CollectionController from "./collectionController";

export function useCollection(databaseName: string, collectionName: string) {
    const PAGE_SIZE = 10;
    const controller = new CollectionController();
    const queryClient = useQueryClient();
    const [message, setMessage] = useState("");
    const [page, setPage] = useState(1);

    const query = useQuery({
        queryKey: [`${databaseName}/${collectionName}`, page],
        queryFn: () => controller.list(databaseName, collectionName, page - 1, PAGE_SIZE),
        placeholderData: keepPreviousData,
    });

    const deleteMutation = useMutation({
        mutationFn: ({ registerId }: { registerId: string }) =>
            controller.delete(databaseName, collectionName, registerId),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: [`${databaseName}/${collectionName}`] });
            setMessage("Register deleted successfully");
        },
        onError: () => {
            setMessage("Error deleting register");
        },
    });

    const updateMutation = useMutation({
        mutationFn: ({ registerId, data }: { registerId: string; data: object }) =>
            controller.update(databaseName, collectionName, registerId, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: [`${databaseName}/${collectionName}`] });
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

    const handlePageChange = (_event: React.ChangeEvent<unknown>, value: number) => {
        setPage(value);
    };

    return {
        query,
        deleteMutation,
        updateMutation,
        page,
        message,
        PAGE_SIZE,
        deleteRegister,
        updateRegister,
        handlePageChange
    };
}
