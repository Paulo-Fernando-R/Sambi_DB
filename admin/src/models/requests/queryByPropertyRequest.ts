import OperatorsEnum from "../enums/operatorsEnum";

export interface QueryByPropertiesConditions {
    propertyName: string;
    value: string | number | boolean;
    operator: OperatorsEnum;
}

export interface QueryByPropertiesRequest {
    databaseName: string;
    collectionName: string;
    limit?: number;
    skip?: number;
    conditionsBehavior?: OperatorsEnum.Or | OperatorsEnum.And;
    queryConditions: QueryByPropertiesConditions[];
}
