import OperatorsEnum from "../models/enums/operatorsEnum";
import { QueryByPropertiesRequest } from "../models/requests/queryByPropertyRequest";
import QueryResponse from "../models/responses/queryResponse";
import { type IcustomAxios } from "../services/IcustomAxios";

export default class QueryRepository {
    private axios: IcustomAxios;
    constructor(axios: IcustomAxios) {
        this.axios = axios;
    }

    async list(data: Pick<QueryByPropertiesRequest, "databaseName" | "collectionName" | "skip">) {

        const request: QueryByPropertiesRequest = {
            databaseName: data.databaseName,
            collectionName: data.collectionName,
            limit: 10,
            skip: data.skip,
            conditionsBehavior: OperatorsEnum.And,
            queryConditions: []
        }

        try {
            const response = await this.axios.instance.post<QueryResponse[]>(`/Query/ByProperty/${request.databaseName}`, request);


            return response.data;
        }
        catch (error) {
            console.log(error);
            // throw error;
        }
    }


}   