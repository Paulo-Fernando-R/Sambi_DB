export default interface IcollectionRepository {
  getCollections(databaseName: string): Promise<string[]>;
  createCollection(databaseName: string, collectionName: string): Promise<void>;
  dropCollection(databaseName: string, collectionName: string): Promise<void>;
}
