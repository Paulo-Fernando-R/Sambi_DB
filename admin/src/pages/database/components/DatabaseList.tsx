import DatabaseResponse from "../../../models/responses/databaseResponse";
import DatabaseListItem from "./DatabaseListItem";

interface DatabaseListProps {
  data?: DatabaseResponse[];
  onDropDatabase: (name: string) => void;
  onDropCollection: (dbName: string, colName: string) => void;
  onCreateCollection: (dbName: string, colName: string) => void;
}

export default function DatabaseList({
  data,
  onDropDatabase,
  onDropCollection,
  onCreateCollection,
}: DatabaseListProps) {
  return (
    <>
      {data?.map((database) => (
        <DatabaseListItem
          key={database.databaseName}
          database={database}
          onDropDatabase={onDropDatabase}
          onDropCollection={onDropCollection}
          onCreateCollection={onCreateCollection}
        />
      ))}
    </>
  );
}
