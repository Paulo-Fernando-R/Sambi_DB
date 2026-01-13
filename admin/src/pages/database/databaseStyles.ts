import { SxProps, Theme } from "@mui/system";
import colors from "../../styles/colors";
import texts from "../../styles/texts";

const page: SxProps<Theme> = {
  padding: "20px",
  paddingTop: "32px",
  minHeight: "100dvh",
  marginLeft: "clamp(100px, 25%, 380px)",
  display: "flex",
  flexDirection: "column",
  gap: "10px",
};

const titleBox: SxProps<Theme> = {
  display: "flex",
  height: "96px",
  alignItems: "center",
  justifyContent: "space-between",
};

const title: SxProps<Theme> = {
  ...texts.title2,
  fontWeight: 600,
  color: colors.text[700],
};

const subtitle: SxProps<Theme> = {
  ...texts.subtitle1,
  fontWeight: 600,
  color: colors.text[700],
};

const listBox: SxProps<Theme> = {
  display: "flex",
  flexDirection: "column",
  gap: "10px",
};

const paper: SxProps<Theme> = {
  display: "flex",
  flexDirection: "column",
  gap: "10px",
  borderRadius: "10px",
  height: "fit-content",
  overflow: "auto",
  // position: "relative",
  padding: "10px",
};

const databaseStyles = {
  page,
  title,
  titleBox,
  listBox,
  paper,
  subtitle,
};
export default databaseStyles;
