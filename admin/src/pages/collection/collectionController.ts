import { type IcustomAxios } from "../../services/IcustomAxios";
import CustomAxios from "../../services/customAxios";
import QueryRepository from "../../repositories/queryRepository";

export default class CollectionController {
    axios: IcustomAxios;
    queryRepository: QueryRepository;
    constructor() {
        this.axios = new CustomAxios();
        this.queryRepository = new QueryRepository(this.axios);
    }

    async list(databaseName: string, collectionName: string, page: number) {
        try {
            const response = await this.queryRepository.list({
                databaseName,
                collectionName,
                skip: page * 10
            })
            return response;
        }
        catch (error) {
            console.log(error);
            throw error;
        }
    }
}   