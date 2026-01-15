/* eslint-disable @typescript-eslint/no-unused-vars */
import * as React from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import useMediaQuery from "@mui/material/useMediaQuery";
import { useTheme } from "@mui/material/styles";
import listItemStyles from "../../../components/listItem/listItemStyles";
import { Paper } from "@mui/material";
import { InteractiveJsonEditor, jsonToEntity } from "interactive-json-editor";
import QuestionDialog from "../../../components/questionDialog/QuestionDialog";
import { Fragment, useCallback, useState } from "react";

export type AddModalProps = {
    open: boolean;
    setOpen: React.Dispatch<React.SetStateAction<boolean>>;
    saveRegister: (json: object) => void;
};

export default function AddModal({ open, setOpen, saveRegister }: AddModalProps) {
    const theme = useTheme();
    const fullScreen = useMediaQuery(theme.breakpoints.down("md"));
    const [entity, setEntity] = useState(jsonToEntity({}));
    const [__, setExtractedJson] = useState("");
    const [updateOpen, setUpdateOpen] = useState(false);
    let finalJson = "";
    const [_, setError] = useState<Error | null>(null);
    const handleExtract = useCallback((json: string, error: Error | null) => {
        setExtractedJson(json || "");
        finalJson = json || "";
        setError(error);
    }, []);

    const handleChange = useCallback((newEntity: object) => {
        setEntity(newEntity);
    }, []);

    const handleClose = () => {
        setOpen(false);
    };

    const handleSave = useCallback(() => {
        if (!finalJson) {
            return;
        }
        saveRegister(JSON.parse(finalJson));
        setUpdateOpen(false);
        setOpen(false);
    }, []);

    return (
        <Fragment>
            <Dialog
                fullScreen={fullScreen}
                open={open}
                onClose={handleClose}
                aria-labelledby="responsive-dialog-title"
            >
                <DialogTitle id="responsive-dialog-title">Add Register</DialogTitle>
                <DialogContent>
                    <DialogContentText>Add a new register to the collection</DialogContentText>
                </DialogContent>

                <Paper elevation={3} sx={listItemStyles.container} className="list-item">
                    <InteractiveJsonEditor
                        initialEntity={entity}
                        theme={listItemStyles.theme}
                        onChange={handleChange}
                        onExtract={handleExtract}
                        minWidth={200}
                        maxHeight={400}
                        width="100%"
                    />

                    <QuestionDialog
                        open={updateOpen}
                        setOpen={setUpdateOpen}
                        title=" Add Register"
                        description="Are you sure you want to add this register?"
                        onConfirm={handleSave}
                        onCancel={() => setUpdateOpen(false)}
                    />
                </Paper>

                <DialogActions>
                    <Button autoFocus onClick={handleClose}>
                        Cancel
                    </Button>
                    <Button onClick={() => setUpdateOpen(true)} autoFocus>
                        Save
                    </Button>
                </DialogActions>
            </Dialog>
        </Fragment>
    );
}
