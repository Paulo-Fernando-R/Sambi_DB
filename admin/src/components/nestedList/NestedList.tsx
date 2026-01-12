import { Collapse, List, ListItemButton, ListItemText } from "@mui/material";
import nestedListStyles from "./nestedListStyles";
import { ExpandMore, ExpandLess } from "@mui/icons-material";
import { useState } from "react";
import { useNavigate } from "react-router";

export default function NestedList() {
    const navigate = useNavigate();
    const [open, setOpen] = useState(false);

    const placeholder =
    {
        database: "paulo",
        collections: [
            {
                collection: "jogos",

            },
            {
                collection: "sites",

            }
        ]
    }

    function navigateToCollection(collection: string) {
        navigate(`/${placeholder.database}/${collection}`);
    }


    const handleOpen = () => setOpen(!open);
    return (
        <List>
            <ListItemButton sx={nestedListStyles.listButton} onClick={handleOpen}>
                <ListItemText primary={placeholder.database} />
                {
                    open ? <ExpandLess /> : <ExpandMore />
                }

            </ListItemButton>
            {
                placeholder.collections.map((collection, index) => (
                    <NestedListItem key={index} open={open} placeholder={collection.collection} navigate={navigateToCollection} />
                ))
            }
        </List>
    );
}

function NestedListItem({ open, placeholder, navigate }: { open: boolean, placeholder: string, navigate: (collection: string) => void }) {
    return (
        <Collapse in={open} timeout="auto" unmountOnExit>
            <List component="div" disablePadding>
                <ListItemButton sx={nestedListStyles.nestedText} onClick={() => navigate(placeholder)}>
                    <ListItemText primary={placeholder} />
                </ListItemButton>
            </List>
        </Collapse>
    );
}
