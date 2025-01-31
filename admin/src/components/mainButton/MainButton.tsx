import { Button } from "@mui/material";
import colors from "../../styles/colors";

type MainButtonProps = {
    text: string;
    onClick?: VoidFunction;
    icon?: React.ReactNode;
};

export default function MainButton({ text, onClick, icon }: MainButtonProps) {
    return (
        <Button
            sx={{
                borderRadius: "12px",
                backgroundColor: colors.primary[800],
                color: colors.bg[100],
                border: "none",
                outline: "none",
                stroke: "none",
            }}
            size="large"
            variant="contained"
            startIcon={icon}
            onClick={onClick}
        >
            {text}
        </Button>
    );
}
