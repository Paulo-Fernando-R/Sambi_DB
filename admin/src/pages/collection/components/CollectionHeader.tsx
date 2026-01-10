import { Box, Typography } from "@mui/material";
import MainButton from "../../../components/mainButton/MainButton";
import { AddCircle } from "@mui/icons-material";
import collectionStyles from "../collectionStyles";
import AddModal from "./AddModal";

interface CollectionHeaderProps {
    databaseName: string;
    collectionName: string;
}

export default function CollectionHeader({ databaseName, collectionName }: CollectionHeaderProps) {
    return (
        <Box sx={collectionStyles.titleBox}>
            <AddModal />
            <Typography sx={collectionStyles.title} variant="h1">
                Database: {databaseName} {`>`} Collection: {collectionName}
            </Typography>
            <MainButton text="ADD DATA" icon={<AddCircle sx={{ fontSize: "24px" }} />} />
        </Box>
    );
}
