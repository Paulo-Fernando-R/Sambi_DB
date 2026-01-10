import { Box } from "@mui/material";
import ListItem, { ListEmpty, ListItemSkeleton } from "../../../components/listItem/ListItem";
import collectionStyles from "../collectionStyles";

interface CollectionListProps {
    data: any[] | undefined;
    isLoading: boolean;
    deleteRegister: (registerId: string) => void;
    updateRegister: (registerId: string, data: object) => void;
}

export default function CollectionList({
    data,
    isLoading,
    deleteRegister,
    updateRegister,
}: CollectionListProps) {
    return (
        <Box sx={collectionStyles.listBox}>
            {isLoading && <ListItemSkeleton />}
            {data?.length === 0 && <ListEmpty />}
            {data?.map((item, index) => (
                <ListItem
                    key={index}
                    data={item!}
                    deleteRegister={deleteRegister}
                    updateRegister={updateRegister}
                />
            ))}
        </Box>
    );
}
