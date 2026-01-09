type QueryResponse = {
    Id: string;
    Data: Record<string, string | number | boolean | Date | Array<any> | Object>
}

export default QueryResponse
