import Drawer from "@mui/material/Drawer";
import sideMenuStyles from "./sideMenuStyles";
import { AddCircle } from "@mui/icons-material";
import sambiIcon from "../../assets/sambi.svg";
import { ImDatabase } from "react-icons/im";
import NestedList from "../nestedList/NestedList";
import {
  Box,
  Typography,
  Button,
  List,
  Backdrop,
  CircularProgress,
  Snackbar,
  Alert,
} from "@mui/material";
import colors from "../../styles/colors";
import FormDialog from "../formDialog/FormDialog";
import { useSideMenu } from "./useSideMenu";

export default function SideMenu() {
  const {
    open,
    setOpen,
    newDatabaseName,
    setNewDatabaseName,
    message,
    query,
    mutation,
    navigateToDatabases,
    handleCreateDatabase,
    handleClose,
  } = useSideMenu();

  return (
    <Drawer sx={sideMenuStyles.dawer} variant="permanent" anchor="left">
      <Box sx={sideMenuStyles.headerBox}>
        <Box sx={sideMenuStyles.logoBox}>
          <img src={sambiIcon} alt="" style={sideMenuStyles.logoImg} />
          <Typography sx={sideMenuStyles.title1} variant="h1">
            Sambi Admin
          </Typography>
        </Box>
      </Box>

      <Box sx={sideMenuStyles.bodyBox}>
        <Button
          sx={sideMenuStyles.button}
          size="large"
          variant="contained"
          startIcon={<AddCircle sx={{ fontSize: "24px" }} />}
          onClick={() => setOpen(true)}
        >
          New Database
        </Button>

        <Typography
          sx={sideMenuStyles.subtitle}
          variant="h1"
          onClick={navigateToDatabases}
        >
          <ImDatabase color={colors.text[800]} />
          Databases
        </Typography>

        <List sx={sideMenuStyles.list}>
          {query.data?.map((database) => (
            <NestedList key={database.databaseName} data={database} />
          ))}
        </List>
      </Box>

      <FormDialog
        open={open}
        setOpen={setOpen}
        title="New Database"
        description="Enter database name"
        onConfirm={handleCreateDatabase}
        onCancel={() => setOpen(false)}
        placeholder="Database name"
        type="text"
        value={newDatabaseName}
        setValue={setNewDatabaseName}
      />

      <Backdrop
        sx={(theme) => ({ color: "#fff", zIndex: theme.zIndex.drawer + 1 })}
        open={mutation.isPending || query.isFetching}
      >
        <CircularProgress color="inherit" />
      </Backdrop>

      <Snackbar
        open={mutation.isSuccess}
        autoHideDuration={6000}
        onClose={handleClose}
      >
        <Alert severity="success" variant="filled" sx={{ width: "100%" }}>
          {message}
        </Alert>
      </Snackbar>

      <Snackbar
        open={mutation.isError}
        autoHideDuration={6000}
        onClose={handleClose}
      >
        <Alert severity="error" variant="filled" sx={{ width: "100%" }}>
          {message}
        </Alert>
      </Snackbar>
    </Drawer>
  );
}
