import get from "@/utils/API/get";
import { useCallback, useEffect, useRef, useState } from "react";

export default function useLazyContainter(url, loadFactor, params, dataProcessor) {
    const [items, setItems] = useState([]);

    const getItems = useCallback(async (skip, take) => {
        try {
            const items = await get({
                skip,
                take,
                ...params,
            }, url);

            return items.value.data;
        }
        catch (error) {
            console.log("Error requesting new items", error);
            return [];
        }
    }, [url, params]);

    const addNewItems = useCallback(async (newItems, clear = false) => {
        if(dataProcessor)
            newItems = newItems.map(dataProcessor);

        if(clear)
            setItems(newItems);

        setItems(items => {
            const merged = [
                ...newItems,
                ...items
            ]

            const seen = new Set();
            return merged.filter(item => {  
                if (seen.has(item.id))
                    return false;
                seen.add(item.id);
                return true;
            })
        });
    }, [dataProcessor]);

    const addNewItem = useCallback(async (newItem) => {
        addNewItems([newItem]);
    }, [addNewItems]);

    const loadMore = useCallback(async (skip, take) => {
        console.log("load");

        const newItems = await getItems(skip, take);
        addNewItems(newItems, skip === 0);

    }, [getItems, addNewItems]);


    const skipRef = useRef(0);
    const containerRef = useRef(null);
    const loadingMoreRef = useRef(false);

    useEffect(() => {
        const init = async () => {
            await loadMore(0, loadFactor);
            skipRef.current = loadFactor;
        };
        init();

    }, [loadMore, loadFactor]);

    // To be used later to display a spinning icon while loading messages
    const [loadingMore, setLoadingMore] = useState(false);

    const handleLoadMore = async () => {
        if (loadingMoreRef.current) return;

        setLoadingMore(true);
        loadingMoreRef.current = true;

        await loadMore(skipRef.current, loadFactor);
        skipRef.current += loadFactor;

        setLoadingMore(false);
        loadingMoreRef.current = false;
    };

    const handleScroll = () => {
        const el = containerRef.current;
        if (!el) return;

        if (el.scrollTop + el.clientHeight > el.scrollHeight - 100) {
            handleLoadMore();
        }
    };

    return [items, containerRef, handleScroll, addNewItem];
}