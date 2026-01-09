import { Box, Skeleton, Typography } from "@mui/material";
import collectionStyles from "./collectionStyles";
import MainButton from "../../components/mainButton/MainButton";
import { AddCircle } from "@mui/icons-material";
import ListItem, { ListEmpty, ListItemSkeleton } from "../../components/listItem/ListItem";
import CollectionController from "./collectionController";
import { useInfiniteQuery } from "@tanstack/react-query";

export default function Collection() {
    const controller = new CollectionController();
    const path = ["paulo", "jogos"];



    const infiniteQuery = useInfiniteQuery({
        queryKey: [path.join("/")],
        queryFn: () => controller.list(path[0], path[1], 0),
        getNextPageParam: (lastPage, allPages) => {
            return 0;
        },
        initialPageParam: 0,

    })

    return (
        <Box sx={collectionStyles.page}>
            <Box sx={collectionStyles.titleBox}>
                <Typography sx={collectionStyles.title} variant="h1">
                    Database: {path[0]} {`>`} Collection: {path[1]}
                </Typography>
                <MainButton text="ADD DATA" icon={<AddCircle sx={{ fontSize: "24px" }} />} />
            </Box>


            <Box sx={collectionStyles.listBox}>
                {infiniteQuery.isFetching &&
                    <ListItemSkeleton />
                }
                {infiniteQuery.data?.pages.flat().length === 0 &&
                    <ListEmpty />
                }
                {infiniteQuery.data?.pages.flat().map((item, index) => (
                    <ListItem key={index} data={item!} />
                ))}
            </Box>
        </Box>
    );
}
