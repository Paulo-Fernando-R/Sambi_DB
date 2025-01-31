/* eslint-disable @typescript-eslint/no-unused-vars */
//@ts-expect-error no types
import { InteractiveJsonEditor, jsonToEntity } from "interactive-json-editor";
import { Box } from "@mui/material";
import listItemStyles from "./listItemStyles";
import { useState, useCallback } from "react";
import useContextMenu from "../../hooks/useContextMenu";
import ContextMenu from "../../contextMenu/ContextMenu";
import ContextMenuItem from "../../contextMenu/ContextMenuItem";
import { Delete, CheckBoxRounded, Undo } from "@mui/icons-material";
import colors from "../../styles/colors";

export default function ListItem() {
    const [_, setExtractedJson] = useState("");
    const [__, setError] = useState<Error | null>(null);
    const context = useContextMenu("list-item");

    const [entity, setEntity] = useState(
        jsonToEntity({
            name: "John Doe",
            age: 30,
            address: {
                street: "123 Main St",
                city: "Springfield",
                state: "IL",
            },
            access: ["read", "write"],
        })
    );

    const handleExtract = useCallback((json: string, error: Error | null) => {
        setExtractedJson(json || "");
        setError(error);
    }, []);

    const handleChange = useCallback((newEntity: object) => {
        setEntity(newEntity);
    }, []);

    return (
        <Box sx={listItemStyles.container} className="list-item">
            <InteractiveJsonEditor
                initialEntity={entity}
                theme={listItemStyles.theme}
                onChange={handleChange}
                onExtract={handleExtract}
                minWidth={1000}
                maxHeight={500}
                width="100%"
            />

            {context.visible && (
                <ContextMenu
                    context={context}
                    children={[
                        <ContextMenuItem
                            text="Delete"
                            icon={<Delete sx={{ color: colors.warning }} />}
                            onClick={() => console.log("Delete")}
                        />,
                        <ContextMenuItem
                            text="Discard Changes"
                            icon={<Undo sx={{ color: colors.text[700] }} />}
                            onClick={() => console.log("Delete")}
                        />,
                        <ContextMenuItem
                            text="Save Changes"
                            icon={<CheckBoxRounded sx={{ color: colors.primary[800] }} />}
                            onClick={() => console.log("Save")}
                        />,
                    ]}
                />
            )}
        </Box>
    );
}
