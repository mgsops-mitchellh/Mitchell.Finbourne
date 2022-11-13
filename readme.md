# Finbourne memory cache
Implementation of an in-memory cache to add and retrieve items written in .net6.0

# Assumptions and pointers
- The types used in the cache can be any generic types, as they all have a method to get hash code and the underlying implementation uses a dictionary.
- There are two "Add" functions, depending on whether the user wants to know whether an item was removed or not.
- The standard "lock" procedure is used to lock the operation as there are two updates being made which affect both the reading and writing operations.
- The frequency of "Most used" is assumed on both read and write operations.
- Assuming the items used within the cache are managed resources. If these were unmanaged, it would make sense for a user to be forced to use the "Add" that returns the item, so it may dispose of any resources needed.

# Potential improvements
- Extract a class that takes care of the locking operations.
- Implement logging to see if any errors occur.
- Implement further unit testing around the locking operations, could be done if this was an injected dependency as mentioned above.
- 