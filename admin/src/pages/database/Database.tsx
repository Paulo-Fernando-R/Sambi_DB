import {
  Backdrop,
  Box,
  CircularProgress,
  Snackbar,
  Alert,
  Typography,
} from "@mui/material";
import databaseStyles from "./databaseStyles";
import { useDatabase } from "./useDatabase";
import DatabaseList from "./components/DatabaseList";

export default function Database() {
  const {
    query,
    message,
    handleDropDatabase,
    handleDropCollection,
    handleCreateCollection,
    handleClose,
    isLoading,
    isSuccess,
    isError,
  } = useDatabase();

  return (
    <Box sx={databaseStyles.page}>
      <Box sx={databaseStyles.titleBox}>
        <Typography sx={databaseStyles.title} variant="h1">
          Databases
        </Typography>
      </Box>

      <DatabaseList
        data={query.data}
        onDropDatabase={handleDropDatabase}
        onDropCollection={handleDropCollection}
        onCreateCollection={handleCreateCollection}
      />

      <Backdrop
        sx={(theme) => ({ color: "#fff", zIndex: theme.zIndex.drawer + 1 })}
        open={isLoading}
      >
        <CircularProgress color="inherit" />
      </Backdrop>

      <Snackbar open={isSuccess} autoHideDuration={6000} onClose={handleClose}>
        <Alert severity="success" variant="filled" sx={{ width: "100%" }}>
          {message}
        </Alert>
      </Snackbar>

      <Snackbar open={isError} autoHideDuration={6000} onClose={handleClose}>
        <Alert severity="error" variant="filled" sx={{ width: "100%" }}>
          {message}
        </Alert>
      </Snackbar>
    </Box>
  );
}
