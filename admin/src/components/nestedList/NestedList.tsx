import { Collapse, List, ListItemButton, ListItemText } from "@mui/material";
import nestedListStyles from "./nestedListStyles";
import { ExpandMore, ExpandLess } from "@mui/icons-material";
import { useState } from "react";
import { useNavigate } from "react-router";
import DatabaseResponse from "../../models/responses/databaseResponse";

export default function NestedList({ data }: { data: DatabaseResponse }) {
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);

  function navigateToCollection(collection: string) {
    navigate(`/${data.databaseName}/${collection}`);
  }

  const handleOpen = () => setOpen(!open);
  return (
    <List>
      <ListItemButton sx={nestedListStyles.listButton} onClick={handleOpen}>
        <ListItemText primary={data.databaseName} />
        {open ? <ExpandLess /> : <ExpandMore />}
      </ListItemButton>

      {data.collections.map((collection, index) => (
        <NestedListItem
          key={index}
          open={open}
          placeholder={collection}
          navigate={navigateToCollection}
        />
      ))}
    </List>
  );
}

function NestedListItem({
  open,
  placeholder,
  navigate,
}: {
  open: boolean;
  placeholder: string;
  navigate: (collection: string) => void;
}) {
  return (
    <Collapse in={open} timeout="auto" unmountOnExit>
      <List component="div" disablePadding>
        <ListItemButton
          sx={nestedListStyles.nestedText}
          onClick={() => navigate(placeholder)}
        >
          <ListItemText primary={placeholder} />
        </ListItemButton>
      </List>
    </Collapse>
  );
}
