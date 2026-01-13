import CustomError from "../errors/customError";
import { IcustomAxios } from "../services/IcustomAxios";
import IcollectionRepository from "./IcollectionRepository";

export default class CollectionRepository implements IcollectionRepository {
  private axios: IcustomAxios;

  constructor(axios: IcustomAxios) {
    this.axios = axios;
  }

  async getCollections(databaseName: string) {
    try {
      const response = await this.axios.instance.get<string[]>(
        `/Collection/List/${databaseName}`
      );

      if (response.status === 404) {
        throw new CustomError(
          "No collections found",
          response.status,
          response.data.toString()
        );
      }

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
