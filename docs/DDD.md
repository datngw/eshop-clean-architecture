# Project Domain: Business Rules & Logic

This document describes the **business domain** of the project, including the entities, their rules (invariants), and the key processes that drive the system. Use this as a guide to understand *what* the system does, rather than *how* the code is structured.

## 1. Domain Aggregates

The system is built around three main concepts (Aggregates): **Products**, **Baskets**, and **Orders**.

### 1.1. Product
Represents an item available for sale.
*   **Attributes**: Name, Description, Price, Image, Category.
*   **Rules (Invariants)**:
    *   **Price**: Must be non-negative.
    *   **Name**: Cannot be empty.
    *   **Category**: Every product must belong to a Category.
*   **Methods**:
    *   `UpdateDetails`: Modifies descriptive info (Name, Description, Image, Category).
    *   `UpdatePrice`: Changes price, ensuring it never goes below zero.

### 1.2. Basket (Shopping Cart)
Represents a user's temporary collection of items before purchase.
*   **Attributes**: User Identity, List of Items (`BasketProductItem`), Total Price.
*   **Rules**:
    *   **Items**: Contains products with a specific quantity and snapshot price.
    *   **Merging**: If a product is added that already exists in the basket, the quantity is updated (summed) rather than creating a duplicate entry.
    *   **Quantity**: Must always be greater than zero.
*   **Methods**:
    *   `AddBasketProductItem`: Adds an item or updates quantity. checks product price snapshots.
    *   `RemoveBasketProductItem`: Removes a specific product line entirely.
    *   `Clear`: Empties the basket.

### 1.3. Order
Represents a finalized purchase agreement.
*   **Attributes**: User ID, User Information (Snapshot), Order Items, Status, Total Price.
*   **Status Lifecycle**:
    1.  `Pending`: Default state when order is created.
    *(Note: `Draft`, `Completed`, `Cancelled` exist in the system but are currently not transitionable via the domain model).*
*   **Rules**:
    *   **User Information**: Must be captured at the time of order (Name, Phone, Address) as a snapshot.
    *   **Immutability**: Once created, the Order Items cannot be changed (unlike the Basket).
*   **Events**:
    *   `OrderCreatedDomainEvent`: Raised immediately when an order is successfully created.

## 2. Shared Business Concepts (Value Objects)

These are reusable concepts that carry specific validation rules used across the system.

*   **Phone**:
    *   Must be exactly **10 digits**.
    *   Cannot be empty.
*   **Address**:
    *   Requires both **Street** and **City**.
    *   Cannot be partially empty.
*   **UserInformation**:
    *   Composite information comprising Name, Phone, and Address.
    *   Used to snapshot customer details on an Order, independent of their account profile.

## 3. Key Business Processes

### 3.1. Checkout Process
The transition from **Basket** to **Order**.

1.  **Trigger**: User initiates checkout from their Basket.
2.  **Input**: Delivery information (Name, Phone, Address) provided by the user.
3.  **Process**:
    *   The system validates the User's Basket exists and is not empty.
    *   The `CheckoutService` takes the current Basket and the provided User Information.
    *   It creates a new `Order` with the items from the Basket.
    *   The Basket is **Cleared** (emptied) immediately after the Order is created.
4.  **Result**: A new Order ID is generated, and an `OrderCreated` event is published.

## 4. Validations
*   **Input Validation**: Strict rules are applied before processing commands (e.g., Phone must be 10 digits, Price must be positive).
*   **State Constraints**: You cannot have a basket item with 0 quantity. You cannot create a product without a category.

---
*This document reflects the current business logic implemented in the Domain layer.*
