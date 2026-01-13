import DatabaseRepository from "../../repositories/databaseRepository";
import IdatabaseRepository from "../../repositories/IdatabaseRepository";
import CustomAxios from "../../services/customAxios";
import { IcustomAxios } from "../../services/IcustomAxios";
import CollectionRepository from "../../repositories/collectionRepository";
import IcollectionRepository from "../../repositories/IcollectionRepository";
import DatabaseResponse from "../../models/responses/databaseResponse";

export default class DatabaseController {
  private databaseRepository: IdatabaseRepository;
  private collectionRepository: IcollectionRepository;
  private axios: IcustomAxios;

  constructor() {
    this.axios = new CustomAxios();
    this.databaseRepository = new DatabaseRepository(this.axios);
    this.collectionRepository = new CollectionRepository(this.axios);
  }

  async getDatabases() {
    return await this.databaseRepository.getDatabases();
  }

  async getCollections(databaseName: string) {
    return await this.collectionRepository.getCollections(databaseName);
  }

  async getDatabasesResponse(): Promise<DatabaseResponse[]> {
    const databasesResponse: DatabaseResponse[] = [];
    const databases = await this.getDatabases();

    for await (const database of databases) {
      const collections = await this.getCollections(database);
      databasesResponse.push({
        databaseName: database,
        collections: collections.sort((a, b) => a.localeCompare(b)),
      });
    }

    return databasesResponse.sort((a, b) =>
      a.databaseName.localeCompare(b.databaseName)
    );
  }

  async createDatabase(databaseName: string) {
    return await this.databaseRepository.createDatabase(databaseName);
  }

  async createCollection(databaseName: string, collectionName: string) {
    return await this.collectionRepository.createCollection(
      databaseName,
      collectionName
    );
  }

  async dropCollection(databaseName: string, collectionName: string) {
    return await this.collectionRepository.dropCollection(
      databaseName,
      collectionName
    );
  }

  async dropDatabase(databaseName: string) {
    return await this.databaseRepository.dropDatabase(databaseName);
  }
}
