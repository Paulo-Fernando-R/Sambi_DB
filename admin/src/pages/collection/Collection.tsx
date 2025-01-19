
import { Box, Typography } from "@mui/material";

export default function Collection() {
    return (
        <Box
            sx={{
                padding: "20px",
                paddingTop: "32px",
                //backgroundColor: "red",
                minHeight: "100dvh",
                marginLeft: "clamp(100px, 25%, 380px)",
            }}
        >
            <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                <Typography variant="h1">Collection</Typography>
                <Typography variant="h1">Filter</Typography>
            </Box>
           
        </Box>
    );
}
