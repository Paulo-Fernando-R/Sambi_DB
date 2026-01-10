import { Box, Typography } from "@mui/material";
import MainButton from "../../../components/mainButton/MainButton";
import { AddCircle } from "@mui/icons-material";
import collectionStyles from "../collectionStyles";
import AddModal from "./AddModal";
import { useState } from "react";

interface CollectionHeaderProps {
    databaseName: string;
    collectionName: string;
}

export default function CollectionHeader({ databaseName, collectionName }: CollectionHeaderProps) {
    const [open, setOpen] = useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const saveRegister = (register: object) => {
        console.log(register);
    };
    return (
        <Box sx={collectionStyles.titleBox}>
            <AddModal open={open} setOpen={setOpen} saveRegister={saveRegister} />
            <Typography sx={collectionStyles.title} variant="h1">
                Database: {databaseName} {`>`} Collection: {collectionName}
            </Typography>
            <MainButton
                onClick={handleClickOpen}
                text="ADD DATA"
                icon={<AddCircle sx={{ fontSize: "24px" }} />}
            />
        </Box>
    );
}
