import React from "react";

import Paper from "@mui/material/Paper";
import MenuList from "@mui/material/MenuList";
import { UseContextMenu } from "../../hooks/useContextMenu";

type ContextMenuProps = {
    children: React.ReactNode[] | React.ReactNode;
    context: UseContextMenu;
};

export default function ContextMenu({ children, context }: ContextMenuProps) {
    return (
        <Paper
            sx={{
                position: "absolute",
                top: `${context.y}px`,
                left: `${context.x}px`,
                width: 320,
                maxWidth: "100%",
            }}
        >
            <MenuList>{children}</MenuList>
        </Paper>
    );
}
