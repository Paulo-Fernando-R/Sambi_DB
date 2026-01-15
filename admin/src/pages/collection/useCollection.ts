import { keepPreviousData, useMutation, useQuery } from "@tanstack/react-query";
import { useState } from "react";
import CollectionController from "./collectionController";

export function useCollection(databaseName: string, collectionName: string) {
    const PAGE_SIZE = 10;
    const controller = new CollectionController();
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
            //  queryClient.invalidateQueries({ queryKey: [`${databaseName}/${collectionName}`] });
            query.refetch();
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
            //  queryClient.invalidateQueries({ queryKey: [`${databaseName}/${collectionName}`] });
            query.refetch();
            setMessage("Register updated successfully");
        },
        onError: () => {
            setMessage("Error updating register");
        },
    });

    const insertMutation = useMutation({
        mutationFn: ({ data }: { data: object }) =>
            controller.insert(databaseName, collectionName, data),
        onSuccess: () => {
            //  queryClient.invalidateQueries({ queryKey: [`${databaseName}/${collectionName}`] });
            query.refetch();
            setMessage("Register inserted successfully");
        },
        onError: () => {
            setMessage("Error inserting register");
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

    const insertRegister = async (data: object) => {
        insertMutation.mutate({ data });
    };

    return {
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
        handlePageChange
    };
}
