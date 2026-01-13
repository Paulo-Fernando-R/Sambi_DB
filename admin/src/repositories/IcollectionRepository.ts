export default interface IcollectionRepository {
  getCollections(databaseName: string): Promise<string[]>;
}
