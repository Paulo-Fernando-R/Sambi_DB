/* eslint-disable @typescript-eslint/no-unused-vars */
//@ts-expect-error no types
import { InteractiveJsonEditor, jsonToEntity } from "interactive-json-editor";
import { Box } from "@mui/material";
import listItemStyles from "./listItemStyles";
import { useState, useCallback } from "react";
import useContextMenu from "../../hooks/useContextMenu";

export default function ListItem() {
    const [_, setExtractedJson] = useState("");
    const [__, setError] = useState<Error | null>(null);
    const context = useContextMenu();

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
                minWidth={200}
                maxHeight={500}
                width="100%"
            />

            {context.visible && (
                <Box
                    sx={{
                        position: "absolute",
                        top: `${context.y}px`,
                        left: `${context.x}px`,
                        width: "100px",
                        height: "100px",
                        backgroundColor: "red",
                    }}
                />
            )}
        </Box>
    );
}
