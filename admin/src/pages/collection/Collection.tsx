import { Box, Typography } from "@mui/material";
import collectionStyles from "./collectionStyles";
import MainButton from "../../components/mainButton/MainButton";
import { AddCircle } from "@mui/icons-material";
import ListItem from "../../components/listItem/ListItem";
import CollectionController from "./collectionController";
import { useInfiniteQuery } from "@tanstack/react-query";

export default function Collection() {
    const controller = new CollectionController();
    const path = ["paulo", "jogos"];

    function list() {
        controller.list(path[0], path[1], 0);
    }


    const infiniteQuery = useInfiniteQuery({
        queryKey: [path.join("/")],
        queryFn: () => controller.list(path[0], path[1], 0),
        getNextPageParam: (lastPage, allPages) => {
            return 0;
        },
        initialPageParam: 0,

    })

    if (!infiniteQuery.data) {
        return <div>Loading...</div>;
    }

    console.log(infiniteQuery.data.pages.flat());

    return (
        <Box sx={collectionStyles.page}>
            <Box sx={collectionStyles.titleBox}>
                <Typography sx={collectionStyles.title} variant="h1">
                    Wanderlei Database : Wrong idea collection
                </Typography>
                <MainButton text="ADD DATA" icon={<AddCircle sx={{ fontSize: "24px" }} />} />
            </Box>


            <Box sx={collectionStyles.listBox}>
                {infiniteQuery.data?.pages.flat().map((item, index) => (
                    <ListItem key={index} />
                ))}
            </Box>
        </Box>
    );
}
