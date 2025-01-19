import { SxProps, Theme } from "@mui/system";
import colors from "../../styles/colors";

import texts from "../../styles/texts";

const listButton: SxProps<Theme> = {
    ...texts.subtitle1,
    padding: "8px 16px",
    height: "28px",
    borderRadius: "12px"
};

const nestedText: SxProps<Theme> = {
    ...texts.subtitle1,
    color: colors.primary[800],
    padding: "8px 28px",
    height: "28px",
    borderRadius: "12px"
};

const nestedListStyles = {
    listButton,
    nestedText,
};
export default nestedListStyles;
