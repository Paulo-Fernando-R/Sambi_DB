import { Box } from "@mui/material";
import ListItem, {
  ListEmpty,
  ListItemSkeleton,
} from "../../../components/listItem/ListItem";
import collectionStyles from "../collectionStyles";
import QueryResponse from "../../../models/responses/queryResponse";

interface CollectionListProps {
  data: QueryResponse[] | undefined;
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

      {data?.length === 0 || (!data && <ListEmpty />)}

      {data?.map((item, index) => (
        <ListItem
          key={index + item.Id}
          data={item!}
          deleteRegister={deleteRegister}
          updateRegister={updateRegister}
        />
      ))}
    </Box>
  );
}
