import {
  Backdrop,
  Box,
  CircularProgress,
  Snackbar,
  Alert,
  Typography,
  Paper,
  List,
  ListItemButton,
  ListItem,
  ListItemText,
  ListItemIcon,
  Button,
} from "@mui/material";
import databaseStyles from "./databaseStyles";
import { useMutation, useQuery } from "@tanstack/react-query";
import DatabaseController from "./databaseController";
import { ImDatabase } from "react-icons/im";
import colors from "../../styles/colors";
import CollectionsBookmarkIcon from "@mui/icons-material/CollectionsBookmark";
import DeleteIcon from "@mui/icons-material/Delete";
import AddIcon from "@mui/icons-material/Add";
import FormDialog from "../../components/formDialog/FormDialog";
import { useState } from "react";

export default function Database() {
  const databaseController = new DatabaseController();
  const [open, setOpen] = useState(false);
  const [newCollectionName, setNewCollectionName] = useState("");
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

  return (
    <Box sx={databaseStyles.page}>
      <Box sx={databaseStyles.titleBox}>
        <Typography sx={databaseStyles.title} variant="h1">
          Databases
        </Typography>
      </Box>

      {query.data?.map((database) => (
        <Box sx={databaseStyles.listBox}>
          <Paper elevation={3} sx={databaseStyles.paper} className="list-item">
            <Box
              style={{
                display: "flex",
                flexDirection: "row",
                alignItems: "center",
                gap: "10px",
              }}
            >
              <ImDatabase color={colors.text[800]} size={24} />
              <Typography sx={databaseStyles.title} variant="h1">
                {database.databaseName}
              </Typography>

              <Box>
                <Button
                  variant="text"
                  size="small"
                  startIcon={<DeleteIcon />}
                  sx={{ fontSize: "14px", color: colors.warning }}
                  onClick={() => handleDropDatabase(database.databaseName)}
                >
                  Drop Database
                </Button>
              </Box>
            </Box>
            <Box
              style={{
                display: "flex",
                flexDirection: "row",
                alignItems: "center",
                gap: "10px",
                paddingTop: "12px",
                paddingLeft: "16px",
              }}
            >
              {database.collections.length > 0 ? (
                <Typography sx={databaseStyles.subtitle} variant="h1">
                  Collections
                </Typography>
              ) : (
                <Typography sx={databaseStyles.subtitle} variant="h1">
                  No Collections
                </Typography>
              )}

              <Button
                variant="text"
                size="small"
                startIcon={<AddIcon />}
                sx={{
                  fontSize: "14px",
                  color: colors.primary[800],
                }}
                onClick={() => setOpen(true)}
              >
                Create Collection
              </Button>
            </Box>

            <List>
              {database.collections.map((collection) => (
                <ListItem alignItems="flex-start">
                  <ListItemButton>
                    <ListItemIcon>
                      <CollectionsBookmarkIcon />
                    </ListItemIcon>
                    <ListItemText primary={collection} />
                  </ListItemButton>
                  <ListItemButton
                    sx={{ fontSize: "14px", color: colors.warning }}
                    onClick={() =>
                      handleDropCollection(database.databaseName, collection)
                    }
                  >
                    <DeleteIcon />
                    Drop Collection
                  </ListItemButton>
                  <FormDialog
                    open={open}
                    setOpen={setOpen}
                    title="New Collection"
                    description="Enter collection name"
                    onConfirm={() =>
                      handleCreateCollection(
                        database.databaseName,
                        newCollectionName
                      )
                    }
                    onCancel={() => setOpen(false)}
                    placeholder="Collection name"
                    type="text"
                    value={newCollectionName}
                    setValue={setNewCollectionName}
                  />
                </ListItem>
              ))}
            </List>
          </Paper>
        </Box>
      ))}

      <Backdrop
        sx={(theme) => ({ color: "#fff", zIndex: theme.zIndex.drawer + 1 })}
        open={
          dropDatabaseMutation.isPending ||
          dropCollectionMutation.isPending ||
          createCollectionMutation.isPending ||
          query.isFetching
        }
      >
        <CircularProgress color="inherit" />
      </Backdrop>

      <Snackbar
        open={
          dropDatabaseMutation.isSuccess ||
          dropCollectionMutation.isSuccess ||
          createCollectionMutation.isSuccess
        }
        autoHideDuration={6000}
        onClose={handleClose}
      >
        <Alert severity="success" variant="filled" sx={{ width: "100%" }}>
          {message}
        </Alert>
      </Snackbar>

      <Snackbar
        open={
          dropDatabaseMutation.isError ||
          dropCollectionMutation.isError ||
          createCollectionMutation.isError
        }
        autoHideDuration={6000}
        onClose={handleClose}
      >
        <Alert severity="error" variant="filled" sx={{ width: "100%" }}>
          {message}
        </Alert>
      </Snackbar>
    </Box>
  );
}
