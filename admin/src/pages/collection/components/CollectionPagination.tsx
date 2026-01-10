import { Pagination, Stack } from "@mui/material";

interface CollectionPaginationProps {
    page: number;
    count: number;
    onChange: (event: React.ChangeEvent<unknown>, value: number) => void;
}

export default function CollectionPagination({ page, count, onChange }: CollectionPaginationProps) {
    return (
        <Stack spacing={2} alignSelf="center">
            <Stack spacing={2} alignSelf="center">
                <Pagination count={count} page={page} onChange={onChange} />
            </Stack>
        </Stack>
    );
}
