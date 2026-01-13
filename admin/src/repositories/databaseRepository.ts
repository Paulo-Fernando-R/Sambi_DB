import CustomError from "../errors/customError";
import { IcustomAxios } from "../services/IcustomAxios";
import IdatabaseRepository from "./IdatabaseRepository";

export default class DatabaseRepository implements IdatabaseRepository {
  private axios: IcustomAxios;

  constructor(axios: IcustomAxios) {
    this.axios = axios;
  }

  async getDatabases() {
    try {
      const response = await this.axios.instance.get<string[]>(
        "/Database/List"
      );

      if (response.status === 404) {
        throw new CustomError(
          "No databases found",
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

  async createDatabase(databaseName: string) {
    try {
      const response = await this.axios.instance.post<string>(
        "/Database/Create",
        { databaseName }
      );

      if (response.status !== 200) {
        throw new CustomError(
          "Failed to create database",
          response.status,
          response.data.toString()
        );
      }
    } catch (error) {
      if (error instanceof CustomError) {
        throw error;
      }
      throw new CustomError("Failed to create database", 500, error as string);
    }
  }
}
