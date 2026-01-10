/* eslint-disable @typescript-eslint/no-unused-vars */
//@ts-expect-error no types
import { InteractiveJsonEditor, jsonToEntity } from "interactive-json-editor";
import { Box, Button, Paper, Skeleton, Typography } from "@mui/material";
import listItemStyles from "./listItemStyles";
import { useState, useCallback } from "react";
import { Delete, CheckBoxRounded, Undo } from "@mui/icons-material";
import colors from "../../styles/colors";
import QueryResponse from "../../models/responses/queryResponse";
import QuestionDialog from "../questionDialog/QuestionDialog";

export type ListItemProps = {
    data: QueryResponse;
    deleteRegister: (registerId: string) => void;
    updateRegister: (registerId: string, data: object) => void;
};

export default function ListItem({ data, deleteRegister, updateRegister }: ListItemProps) {
    const [__, setExtractedJson] = useState("");
    let finalJson = "";
    const [_, setError] = useState<Error | null>(null);
    const [open, setOpen] = useState(false);
    const [updateOpen, setUpdateOpen] = useState(false);
    const [entity, setEntity] = useState(jsonToEntity(data));

    const handleExtract = useCallback((json: string, error: Error | null) => {
        setExtractedJson(json || "");
        finalJson = json || "";
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

    const handleUpdate = useCallback(() => {
        console.log(finalJson);
        if (!finalJson) {
            setError(null);
            return;
        }
        updateRegister(data.Id, JSON.parse(finalJson).Data);
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
                <Button
                    onClick={() => setOpen(true)}
                    variant="contained"
                    endIcon={<Delete sx={{ color: colors.bg[100] }} />}
                >
                    Delete
                </Button>
                <Button
                    onClick={handleDiscard}
                    variant="contained"
                    endIcon={<Undo sx={{ color: colors.bg[100] }} />}
                >
                    Discard
                </Button>
                <Button
                    onClick={() => setUpdateOpen(true)}
                    variant="contained"
                    endIcon={<CheckBoxRounded sx={{ color: colors.bg[100] }} />}
                >
                    Save
                </Button>
            </Box>

            <QuestionDialog
                open={open}
                setOpen={setOpen}
                title="Delete Register"
                description="Are you sure you want to delete this register?"
                onConfirm={handleDelete}
                onCancel={() => setOpen(false)}
            />

            <QuestionDialog
                open={updateOpen}
                setOpen={setUpdateOpen}
                title="Update Register"
                description="Are you sure you want to update this register?"
                onConfirm={handleUpdate}
                onCancel={() => setUpdateOpen(false)}
            />
        </Paper>
    );
}

export function ListItemSkeleton() {
    return (
        <Box>
            <Skeleton variant="text" sx={{ fontSize: "2rem" }} />
            <Skeleton variant="text" sx={{ fontSize: "3rem" }} />
            <Skeleton variant="rectangular" width={"100%"} height={"50vh"} />
        </Box>
    );
}

export function ListEmpty() {
    return (
        <Box>
            <Typography color="text.secondary" variant="h6">
                No data found for this collection
            </Typography>
            <Typography color="text.secondary" variant="body1">
                Try to add some data
            </Typography>
        </Box>
    );
}
