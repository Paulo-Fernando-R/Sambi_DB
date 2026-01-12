import Drawer from "@mui/material/Drawer";
import sideMenuStyles from "./sideMenuStyles";
import { AddCircle } from "@mui/icons-material";
import sambiIcon from "../../assets/sambi.svg";
import { ImDatabase } from "react-icons/im";
import NestedList from "../nestedList/NestedList";
import { Box, Typography, Button, List } from "@mui/material";
import colors from "../../styles/colors";
import FormDialog from "../formDialog/FormDialog";
import { useState } from "react";

export default function SideMenu() {
  const [open, setOpen] = useState(false);
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

        <Typography sx={sideMenuStyles.subtitle} variant="h1">
          <ImDatabase color={colors.text[800]} />
          Databases
        </Typography>

        <List sx={sideMenuStyles.list}>
          <NestedList />
          <NestedList />
        </List>
      </Box>
      <FormDialog
        open={open}
        setOpen={setOpen}
        title="New Database"
        description="Enter database name"
        onConfirm={() => setOpen(false)}
        onCancel={() => setOpen(false)}
        placeholder="Database name"
        type="text"
      />
    </Drawer>
  );
}
