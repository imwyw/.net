<!-- TOC -->

- [DesignPatterns](#designpatterns)
    - [设计模式](#设计模式)
    - [设计原则](#设计原则)
        - [单一职责原则](#单一职责原则)
        - [开闭原则(Open-Closed Principle)](#开闭原则open-closed-principle)
        - [里氏代替原则(Liskov Substitution Principle)](#里氏代替原则liskov-substitution-principle)
        - [依赖倒置原则](#依赖倒置原则)
        - [接口隔离原则](#接口隔离原则)
        - [合成复用原则](#合成复用原则)
        - [迪米特法则](#迪米特法则)

<!-- /TOC -->
<a id="markdown-designpatterns" name="designpatterns"></a>
# DesignPatterns
<a id="markdown-设计模式" name="设计模式"></a>
## 设计模式
模式设计(Design pattern)是一套被反复使用、多数人知晓的、经过分类编目的、代码设计经验的总结。使用设计模式是为了可重用代码、让代码更容易被他人理解、保证代码可靠性。 毫无疑问，设计模式于己于他人于系统都是多赢的，设计模式使代码编制真正工程化，设计模式是软件工程的基石，如同大厦的一块块砖石一样。

<a id="markdown-设计原则" name="设计原则"></a>
## 设计原则
<a id="markdown-单一职责原则" name="单一职责原则"></a>
### 单一职责原则

就一个类而言，应该只有一个引起它变化的原因。如果一个类承担的职责过多，就等于把这些职责耦合在一起，一个职责的变化可能会影响到其他的职责，另外，把多个职责耦合在一起，也会影响复用性。

<a id="markdown-开闭原则open-closed-principle" name="开闭原则open-closed-principle"></a>
### 开闭原则(Open-Closed Principle)

开闭原则即OCP（Open-Closed Principle缩写）原则，该原则强调的是：一个软件实体（指的类、函数、模块等）应该对扩展开放，对修改关闭。即每次发生变化时，要通过添加新的代码来增强现有类型的行为，而不是修改原有的代码。

符合开闭原则的最好方式是提供一个固有的接口，然后让所有可能发生变化的类实现该接口，让固定的接口与相关对象进行交互。

<a id="markdown-里氏代替原则liskov-substitution-principle" name="里氏代替原则liskov-substitution-principle"></a>
### 里氏代替原则(Liskov Substitution Principle)

Liskov Substitution Principle,LSP（里氏代替原则）指的是子类必须替换掉它们的父类型。也就是说，在软件开发过程中，子类替换父类后，程序的行为是一样的。只有当子类替换掉父类后，此时软件的功能不受影响时，父类才能真正地被复用，而子类也可以在父类的基础上添加新的行为。

<a id="markdown-依赖倒置原则" name="依赖倒置原则"></a>
### 依赖倒置原则

依赖倒置（Dependence Inversion Principle, DIP）原则指的是抽象不应该依赖于细节，细节应该依赖于抽象，也就是提出的 “面向接口编程，而不是面向实现编程”。这样可以降低客户与具体实现的耦合。

<a id="markdown-接口隔离原则" name="接口隔离原则"></a>
### 接口隔离原则

接口隔离原则（Interface Segregation Principle, ISP）指的是使用多个专门的接口比使用单一的总接口要好。也就是说不要让一个单一的接口承担过多的职责，而应把每个职责分离到多个专门的接口中，进行接口分离。过于臃肿的接口是对接口的一种污染。

<a id="markdown-合成复用原则" name="合成复用原则"></a>
### 合成复用原则

合成复用原则（Composite Reuse Principle, CRP）就是在一个新的对象里面使用一些已有的对象，使之成为新对象的一部分。新对象通过向这些对象的委派达到复用已用功能的目的。简单地说，就是要尽量使用合成/聚合，尽量不要使用继承。

<a id="markdown-迪米特法则" name="迪米特法则"></a>
### 迪米特法则

迪米特法则（Law of Demeter，LoD）又叫最少知识原则（Least Knowledge Principle，LKP），指的是一个对象应当对其他对象有尽可能少的了解。也就是说，一个模块或对象应尽量少的与其他实体之间发生相互作用，使得系统功能模块相对独立，这样当一个模块修改时，影响的模块就会越少，扩展起来更加容易。

关于迪米特法则其他的一些表述有：只与你直接的朋友们通信；不要跟“陌生人”说话。

---

参考引用：

[C#设计模式总结](http://www.cnblogs.com/zhili/p/DesignPatternSummery.html)

[图说设计模式](https://design-patterns.readthedocs.io/zh_CN/latest/index.html)
