import * as React from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import { TextField } from "@mui/material";

export type FormDialogProps = {
  title: string;
  description: string;
  placeholder: string;
  confirmText?: string;
  cancelText?: string;
  type: React.HTMLInputTypeAttribute | undefined;
  onConfirm: () => void;
  onCancel: () => void;
  open: boolean;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
};

export default function FormDialog({
  description,
  onConfirm,
  onCancel,
  placeholder,
  type,
  confirmText = "Confirm",
  cancelText = "Cancel",
  title,
  open,
  setOpen,
}: FormDialogProps) {
  const handleClose = () => {
    setOpen(false);
  };

  const handleConfirm = () => {
    onConfirm();
    setOpen(false);
  };

  const handleCancel = () => {
    onCancel();
    setOpen(false);
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const formJson = Object.fromEntries((formData as any).entries());
    const email = formJson.email;
    console.log(email);
    handleClose();
  };

  return (
    <React.Fragment>
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>{title}</DialogTitle>
        <DialogContent>
          <DialogContentText>{description}</DialogContentText>
          <form onSubmit={handleSubmit} id="subscription-form">
            <TextField
              autoFocus
              required
              margin="dense"
              id={placeholder}
              name={placeholder}
              label={placeholder}
              type={type}
              fullWidth
              variant="standard"
            />
          </form>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>{cancelText}</Button>
          <Button type="submit" form="subscription-form">
            {confirmText}
          </Button>
        </DialogActions>
      </Dialog>
    </React.Fragment>
  );
}
