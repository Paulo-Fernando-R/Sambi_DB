import CustomError from "../errors/customError";
import OperatorsEnum from "../models/enums/operatorsEnum";
import { QueryByPropertiesRequest } from "../models/requests/queryByPropertyRequest";
import QueryResponse from "../models/responses/queryResponse";
import { type IcustomAxios } from "../services/IcustomAxios";
import IqueryRepository from "./IqueryRepository";

export default class QueryRepository implements IqueryRepository {
  private axios: IcustomAxios;
  constructor(axios: IcustomAxios) {
    this.axios = axios;
  }

  async list(
    data: Pick<
      QueryByPropertiesRequest,
      "databaseName" | "collectionName" | "skip" | "limit"
    >
  ) {
    const request: QueryByPropertiesRequest = {
      databaseName: data.databaseName,
      collectionName: data.collectionName,
      limit: data.limit,
      skip: data.skip,
      conditionsBehavior: OperatorsEnum.And,
      queryConditions: [],
    };

    try {
      const response = await this.axios.instance.post<QueryResponse[]>(
        `/Query/ByProperty/${request.databaseName}`,
        request
      );

      if (response.status !== 200) {
        throw new CustomError(
          "Failed to fetch data",
          response.status,
          response.data.toString()
        );
      }
      return response.data;
    } catch (error) {
      if (error instanceof CustomError) {
        throw error;
      }
      throw new CustomError("Failed to fetch data", 500, error as string);
    }
  }
}
