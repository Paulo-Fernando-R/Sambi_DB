import { Collapse, List, ListItemButton, ListItemText } from "@mui/material";
import nestedListStyles from "./nestedListStyles";
import { ExpandMore, ExpandLess } from "@mui/icons-material";
import { useState } from "react";
export default function NestedList() {
    const [open, setOpen] = useState(false);

    const handleOpen = () => setOpen(!open);
    return (
        <List>
            <ListItemButton sx={nestedListStyles.listButton} onClick={handleOpen}>
                <ListItemText primary="Database 1" />
                {
                    open ? <ExpandLess /> : <ExpandMore />
                }
                
            </ListItemButton>
            <NestedListItem open={open} />
            <NestedListItem open={open} />
        </List>
    );
}

function NestedListItem({ open }: { open: boolean }) {
    return (
        <Collapse in={open} timeout="auto" unmountOnExit>
            <List component="div" disablePadding>
                <ListItemButton sx={nestedListStyles.nestedText}>
                    <ListItemText primary="Starred" />
                </ListItemButton>
            </List>
        </Collapse>
    );
}
