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
        return [];
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

  async createCollection(databaseName: string, collectionName: string) {
    try {
      const response = await this.axios.instance.post<string>(
        `/Collection/Create/${databaseName}`,
        { collectionName }
      );

      if (response.status !== 200) {
        throw new CustomError(
          "Failed to create collection",
          response.status,
          response.data.toString()
        );
      }
    } catch (error) {
      if (error instanceof CustomError) {
        throw error;
      }
      throw new CustomError(
        "Failed to create collection",
        500,
        error as string
      );
    }
  }

  async dropCollection(databaseName: string, collectionName: string) {
    try {
      const response = await this.axios.instance.delete<string>(
        `/Collection/Delete/${databaseName}`,
        {
          data: { collectionName, confirm: true },
        }
      );

      if (response.status !== 200) {
        throw new CustomError(
          "Failed to drop collection",
          response.status,
          response.data.toString()
        );
      }
    } catch (error) {
      if (error instanceof CustomError) {
        throw error;
      }
      throw new CustomError("Failed to drop collection", 500, error as string);
    }
  }
}
