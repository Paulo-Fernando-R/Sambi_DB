import React from "react";
import MenuItem from "@mui/material/MenuItem";
import ListItemText from "@mui/material/ListItemText";
import ListItemIcon from "@mui/material/ListItemIcon";

type ContextMenuItemProps = {
    text: string;
    icon?: React.ReactNode;
    onClick?: VoidFunction;
};

export default function ContextMenuItem({ text, icon, onClick }: ContextMenuItemProps) {
    return (
        <MenuItem onClick={onClick}>
            <ListItemIcon>{icon}</ListItemIcon>
            <ListItemText>{text}</ListItemText>
        </MenuItem>
    );
}
