import { SxProps, Theme } from "@mui/system";
import colors from "./colors";


const title1: SxProps<Theme> = {
    fontSize: "20px",
    fontWeight: 500,
    color: colors.text[800],
    lineHeight: "3rem",
}
const title2: SxProps<Theme> = {
    fontSize: "16px",
    fontWeight: 400,
    color: colors.text[800],
    lineHeight: "2rem",
}

const texts = {
    title1,
    title2
}

export default texts;