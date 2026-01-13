import { useMutation, useQuery } from "@tanstack/react-query";
import { useState } from "react";
import DatabaseController from "./databaseController";

export function useDatabase() {
  const databaseController = new DatabaseController();
  const [message, setMessage] = useState("");

  const query = useQuery({
    queryKey: ["databases"],
    queryFn: () => databaseController.getDatabasesResponse(),
  });

  const dropDatabaseMutation = useMutation({
    mutationFn: (databaseName: string) =>
      databaseController.dropDatabase(databaseName),
    onSuccess: () => {
      query.refetch();
      setMessage("Database dropped successfully");
    },
    onError: (error) => {
      setMessage(error.message);
    },
  });

  const dropCollectionMutation = useMutation({
    mutationFn: ({
      databaseName,
      collectionName,
    }: {
      databaseName: string;
      collectionName: string;
    }) => databaseController.dropCollection(databaseName, collectionName),
    onSuccess: () => {
      query.refetch();
      setMessage("Collection dropped successfully");
    },
    onError: (error) => {
      setMessage(error.message);
    },
  });

  const createCollectionMutation = useMutation({
    mutationFn: ({
      databaseName,
      collectionName,
    }: {
      databaseName: string;
      collectionName: string;
    }) => databaseController.createCollection(databaseName, collectionName),
    onSuccess: () => {
      query.refetch();
      setMessage("Collection created successfully");
    },
    onError: (error) => {
      setMessage(error.message);
    },
  });

  function handleDropDatabase(databaseName: string) {
    dropDatabaseMutation.mutate(databaseName);
  }

  function handleDropCollection(databaseName: string, collectionName: string) {
    dropCollectionMutation.mutate({ databaseName, collectionName });
  }

  function handleCreateCollection(
    databaseName: string,
    collectionName: string
  ) {
    createCollectionMutation.mutate({ databaseName, collectionName });
  }

  function handleClose() {
    setMessage("");
    dropDatabaseMutation.reset();
    dropCollectionMutation.reset();
    createCollectionMutation.reset();
  }

  const isLoading =
    dropDatabaseMutation.isPending ||
    dropCollectionMutation.isPending ||
    createCollectionMutation.isPending ||
    query.isFetching;

  const isSuccess =
    dropDatabaseMutation.isSuccess ||
    dropCollectionMutation.isSuccess ||
    createCollectionMutation.isSuccess;

  const isError =
    dropDatabaseMutation.isError ||
    dropCollectionMutation.isError ||
    createCollectionMutation.isError;

  return {
    query,
    message,
    handleDropDatabase,
    handleDropCollection,
    handleCreateCollection,
    handleClose,
    isLoading,
    isSuccess,
    isError,
  };
}
