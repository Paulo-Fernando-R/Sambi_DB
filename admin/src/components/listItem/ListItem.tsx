/* eslint-disable @typescript-eslint/no-unused-vars */
//@ts-expect-error no types
import { InteractiveJsonEditor, jsonToEntity } from "interactive-json-editor";
import { Box, Skeleton, Typography } from "@mui/material";
import listItemStyles from "./listItemStyles";
import { useState, useCallback } from "react";
import useContextMenu from "../../hooks/useContextMenu";
import ContextMenu from "../../components/contextMenu/ContextMenu";
import ContextMenuItem from "../../components/contextMenu/ContextMenuItem";
import { Delete, CheckBoxRounded, Undo } from "@mui/icons-material";
import colors from "../../styles/colors";
import QueryResponse from "../../models/responses/queryResponse";

export default function ListItem({ data }: { data: QueryResponse }) {
    const [_, setExtractedJson] = useState("");
    const [__, setError] = useState<Error | null>(null);
    const context = useContextMenu("list-item");


    const [entity, setEntity] = useState(jsonToEntity(data));

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
                initialEntity={data}
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

export function ListItemSkeleton() {
    return (
        <Box>
            <Skeleton variant="text" sx={{ fontSize: '2rem' }} />
            <Skeleton variant="text" sx={{ fontSize: '3rem' }} />
            <Skeleton variant="rectangular" width={"100%"} height={"50vh"} />
        </Box>
    );
}

export function ListEmpty() {
    return (
        <Box>
            <Typography color="text.secondary" variant="h6">No data found for this collection</Typography>
            <Typography color="text.secondary" variant="body1">Try to add some data</Typography>

        </Box>
    );
}