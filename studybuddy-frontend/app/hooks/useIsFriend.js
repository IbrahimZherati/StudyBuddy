import useGetDataList from "./useGetDataList";

export default function useIsFriend(id) {
    const myFriends = useGetDataList("ClientUser/GetFriends");

    return myFriends?.filter(friend => friend.id == id).length > 0;
}