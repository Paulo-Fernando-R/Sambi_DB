import { useState, useEffect } from "react";

const useContextMenu = () => {
    const [context, setContext] = useState({ x: 0, y: 0, visible: false });
    const open = () =>
        document.addEventListener("contextmenu", (event) => {
            event.preventDefault();
            setContext({ x: event.pageX, y: event.pageY, visible: true });
        });

    const close = () =>
        document.addEventListener("click", () => {
            setContext({ x: 0, y: 0, visible: false });
        });

    useEffect(() => {
        open();
        close();
        return () => {
            document.removeEventListener("contextmenu", open);
            document.removeEventListener("click", close);
        };
    }, []);

    return context;
};


export default useContextMenu;