import { QueryByPropertiesRequest } from "../models/requests/queryByPropertyRequest";
import QueryResponse from "../models/responses/queryResponse";

export default interface IqueryRepository {
    list(data: Pick<QueryByPropertiesRequest, "databaseName" | "collectionName" | "skip" | "limit">): Promise<QueryResponse[]>
}