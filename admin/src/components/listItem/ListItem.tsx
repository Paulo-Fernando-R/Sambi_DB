/* eslint-disable @typescript-eslint/no-unused-vars */
//@ts-expect-error no types
import { InteractiveJsonEditor, jsonToEntity } from "interactive-json-editor";
import { Box, Button, Paper, Skeleton, Typography } from "@mui/material";
import listItemStyles from "./listItemStyles";
import { useState, useCallback } from "react";
import useContextMenu from "../../hooks/useContextMenu";
import { Delete, CheckBoxRounded, Undo } from "@mui/icons-material";
import colors from "../../styles/colors";
import QueryResponse from "../../models/responses/queryResponse";

export type ListItemProps = {
    data: QueryResponse;
    deleteRegister: (registerId: string) => void;
}

export default function ListItem({ data, deleteRegister }: ListItemProps) {
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

    const handleDiscard = useCallback(() => {
        setEntity(jsonToEntity(data));
    }, []);

    const handleDelete = useCallback(() => {
        deleteRegister(data.Id);
    }, []);

    return (
        <Paper elevation={3} sx={listItemStyles.container} className="list-item">
            <InteractiveJsonEditor
                initialEntity={entity}
                theme={listItemStyles.theme}
                onChange={handleChange}
                onExtract={handleExtract}
                minWidth={200}
                maxHeight={500}
                width="100%"
            />
            <Box sx={listItemStyles.buttonContainer}>
                <Button onClick={handleDelete} variant="contained" endIcon={<Delete sx={{ color: colors.bg[100] }} />}>
                    Delete
                </Button>
                <Button onClick={handleDiscard} variant="contained" endIcon={<Undo sx={{ color: colors.bg[100] }} />}>
                    Discard
                </Button>
                <Button onClick={handleDiscard} variant="contained" endIcon={<CheckBoxRounded sx={{ color: colors.bg[100] }} />}>
                    Save
                </Button>

            </Box>

            {/* {context.visible && (
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
            )} */}
        </Paper>
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