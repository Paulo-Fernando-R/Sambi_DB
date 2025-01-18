import { SxProps, Theme } from "@mui/system";
import colors from "../../styles/colors";
import { CSSProperties } from "react";
import texts from "../../styles/texts";

const dawer: SxProps<Theme> = {
    width: "25%",
    maxWidth: "380px",
    flexShrink: 200,
    "& .MuiDrawer-paper": {
        width: "25%",
        maxWidth: "380px",
        boxSizing: "border-box",
        backgroundColor: colors.bg[200],
        borderColor: colors.stroke,
        gap: "12px"
    },
};

const headerBox: SxProps<Theme> = {
    padding: "32px 20px 20px 20px",
    borderBottom: `1px solid ${colors.stroke}`,
};

const logoBox: SxProps<Theme> = {
    height: "96px",
    border: `3px solid ${colors.stroke}`,
    borderRadius: "20px",
    display: "flex",
    alignItems: "center",
    justifyContent: "flex-start",
    padding:"20px",
    gap: "12px"
};

const logoImg: CSSProperties = {
    width: "56px",
    height: "56px",
    objectFit: "contain",
}
const title1 = texts.title1;

const bodyBox: SxProps<Theme> = {
    padding: "20px",
    gap: "28px",
    display: "flex",
    flexDirection: "column",
    
}

const button: SxProps<Theme> = {
    borderRadius: "12px",
    backgroundColor: colors.primary[800],
    color: colors.bg[100],
    border: "none",
    outline: "none",
    stroke: "none"
}

const subtitle: SxProps<Theme> = {
    ...texts.title2,
    display: "flex",
    alignItems: "center",
    gap: "10px"
}

const sideMenuStyles = {
    dawer,
    headerBox,
    logoBox,
    logoImg,
    title1,
    bodyBox,
    button,
    subtitle
};



export default sideMenuStyles;
