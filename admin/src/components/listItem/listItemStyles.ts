import { SxProps, Theme } from "@mui/system";
import colors from "../../styles/colors";

const container: SxProps<Theme> = {
    display: "flex",
    flexDirection: "row",
    gap: "10px",
    borderRadius: "10px",
    height: "fit-content",
    overflow: "auto",
   // position: "relative",
};

const theme = {
    background: "#ffffff",
    text: "#333333",
    border: colors.stroke,
    button: "#4a90e2",
    buttonText: "#ffffff",
    warning: "#ffeeba",
    warningBorder: "#ff7100",
    warningText: "#856404",
    error: "#da3a49",
    errorText: "#721c24",
    placeholder: "#999999",
};

const listItemStyles = {
    container,
    theme,
};
export default listItemStyles;
