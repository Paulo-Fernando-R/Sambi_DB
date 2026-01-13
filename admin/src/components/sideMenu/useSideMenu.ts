import { useState } from "react";
import { useNavigate } from "react-router";
import { useMutation, useQuery } from "@tanstack/react-query";
import SideMenuController from "./sideMenuController";

export function useSideMenu() {
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);
  const [newDatabaseName, setNewDatabaseName] = useState("");
  const [message, setMessage] = useState("");

  const sideMenuController = new SideMenuController();

  const query = useQuery({
    queryKey: ["databases"],
    queryFn: () => sideMenuController.getDatabasesResponse(),
  });

  const mutation = useMutation({
    mutationFn: () => sideMenuController.createDatabase(newDatabaseName),
    onSuccess: () => {
      setOpen(false);
      setMessage("Database created successfully");
      query.refetch();
    },
    onError: () => {
      setOpen(false);
      setMessage("Database creation failed");
    },
  });

  function navigateToDatabases() {
    navigate("/");
  }

  function handleCreateDatabase() {
    mutation.mutate();
  }

  function handleClose() {
    mutation.reset();
  }

  return {
    open,
    setOpen,
    newDatabaseName,
    setNewDatabaseName,
    message,
    query,
    mutation, // Included to access isPending, isSuccess, isError
    navigateToDatabases,
    handleCreateDatabase,
    handleClose,
  };
}
