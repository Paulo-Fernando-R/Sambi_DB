import { type IcustomAxios } from "../../services/IcustomAxios";
import CustomAxios from "../../services/customAxios";
import IqueryRepository from "../../repositories/IqueryRepository";
import IregisterRepository from "../../repositories/IregisterRepository";
import QueryRepository from "../../repositories/queryRepository";
import RegisterRepository from "../../repositories/registerRepository";

export default class CollectionController {
    axios: IcustomAxios;
    queryRepository: IqueryRepository;
    registerRepository: IregisterRepository;
    constructor() {
        this.axios = new CustomAxios();
        this.queryRepository = new QueryRepository(this.axios);
        this.registerRepository = new RegisterRepository(this.axios);
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

    async delete(databaseName: string, collectionName: string, registerId: string) {
        try {
            await this.registerRepository.delete(databaseName, collectionName, registerId);
        }
        catch (error) {
            console.log(error);
            throw error;
        }
    }

    async update(databaseName: string, collectionName: string, registerId: string, data: object) {
        try {
            await this.registerRepository.update(databaseName, collectionName, registerId, data);
        }
        catch (error) {
            console.log(error);
            throw error;
        }
    }
}   