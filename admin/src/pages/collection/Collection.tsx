import { Box, Typography } from "@mui/material";
import collectionStyles from "./collectionStyles";
import MainButton from "../../components/mainButton/MainButton";
import { AddCircle } from "@mui/icons-material";
import ListItem from "../../components/listItem/ListItem";

export default function Collection() {
    return (
        <Box sx={collectionStyles.page}>
            <Box sx={collectionStyles.titleBox}>
                <Typography sx={collectionStyles.title} variant="h1">
                    Wanderlei Database : Wrong idea collection
                </Typography>
                <MainButton text="ADD DATA" icon={<AddCircle sx={{ fontSize: "24px" }} />} />
            </Box>

            <Box sx={collectionStyles.listBox}>
                <ListItem/>
                <ListItem/>
            </Box>
        </Box>
    );
}
