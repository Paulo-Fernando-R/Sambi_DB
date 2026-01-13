import {
  Box,
  Paper,
  Typography,
  Button,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import { ImDatabase } from "react-icons/im";
import DeleteIcon from "@mui/icons-material/Delete";
import AddIcon from "@mui/icons-material/Add";
import CollectionsBookmarkIcon from "@mui/icons-material/CollectionsBookmark";
import { useState } from "react";
import FormDialog from "../../../components/formDialog/FormDialog";
import databaseStyles from "../databaseStyles";
import colors from "../../../styles/colors";
import DatabaseResponse from "../../../models/responses/databaseResponse";

interface DatabaseListItemProps {
  database: DatabaseResponse;
  onDropDatabase: (name: string) => void;
  onDropCollection: (dbName: string, colName: string) => void;
  onCreateCollection: (dbName: string, colName: string) => void;
}

export default function DatabaseListItem({
  database,
  onDropDatabase,
  onDropCollection,
  onCreateCollection,
}: DatabaseListItemProps) {
  const [open, setOpen] = useState(false);
  const [newCollectionName, setNewCollectionName] = useState("");

  const handleCreate = () => {
    onCreateCollection(database.databaseName, newCollectionName);
    setOpen(false);
    setNewCollectionName("");
  };

  return (
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
              onClick={() => onDropDatabase(database.databaseName)}
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
            <ListItem alignItems="flex-start" key={collection}>
              <ListItemButton>
                <ListItemIcon>
                  <CollectionsBookmarkIcon />
                </ListItemIcon>
                <ListItemText primary={collection} />
              </ListItemButton>
              <ListItemButton
                sx={{ fontSize: "14px", color: colors.warning }}
                onClick={() =>
                  onDropCollection(database.databaseName, collection)
                }
              >
                <DeleteIcon />
                Drop Collection
              </ListItemButton>
            </ListItem>
          ))}
        </List>
        <FormDialog
          open={open}
          setOpen={setOpen}
          title="New Collection"
          description="Enter collection name"
          onConfirm={handleCreate}
          onCancel={() => setOpen(false)}
          placeholder="Collection name"
          type="text"
          value={newCollectionName}
          setValue={setNewCollectionName}
        />
      </Paper>
    </Box>
  );
}
